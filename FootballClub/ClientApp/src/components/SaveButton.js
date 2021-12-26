import React from 'react';
import $ from 'jquery';

import * as UrlParser from './UrlParser';
import * as SchemaProvider from './Providers/SchemaProvider';
import * as EntityProvider from './Providers/EntityProvider';

export default function SaveButton(props) {
    async function normalizeSerializedArray(serializedArray, entityName, personId) {
        if (!serializedArray) {
            throw new Error("Serialized array is not defined");
        }

        SchemaProvider.validateEntityName(entityName);

        let entitySchema = await SchemaProvider.getSchema(entityName);
        let entityId;
        let entity;

        if (entityName === "Persons")
            entityId = personId;
        else
            entityId = UrlParser.getEntityIdFromUrlForCardPage();


        if (props.insertingMode)
            entity = await EntityProvider.getEmptyEntity(entityName);
        else
            entity = await EntityProvider.getEntity(entityName, entityId);

        await entitySchema.forEach(column => {
            const dataBaseColumnName = column.dataBaseColumnName;
            const columnName = dataBaseColumnName[0].toLowerCase() + dataBaseColumnName.slice(1);
            const columnDataType = column.dataType;

            const columnInSerializedArray = serializedArray.find(item => item.name === dataBaseColumnName);

            if (columnInSerializedArray) {
                let columnValueInSerializedArray = columnInSerializedArray.value;

                if (!columnValueInSerializedArray) {
                    columnValueInSerializedArray = null;
                }

                const isDate = (columnValueInSerializedArray !== null) && (columnDataType === "datetime");

                if (isDate) {
                    const splittedValue = columnValueInSerializedArray.split('/');
                    columnValueInSerializedArray = `${splittedValue[2]}-${splittedValue[1]}-${splittedValue[0]}`;
                }

                const isBoolean = (columnValueInSerializedArray !== null) && (columnDataType === "tinyint");

                if (isBoolean) {
                    columnValueInSerializedArray = columnValueInSerializedArray === "Да" ? true : false;
                }

                entity[`${columnName}`] = columnValueInSerializedArray;
            }
        });

        return entity;
    }

    async function save() {
        let entitySerializedInArray = $('#entity-info-form').serializeArray();
        let personSerializedInArray = $('#person-info-form').serializeArray();

        const entityName = UrlParser.getEntityNameFromUrlForCardPage();

        let entity = await normalizeSerializedArray(entitySerializedInArray, entityName);

        if (personSerializedInArray.length > 0) {
            let personId;

            if (!props.insertingMode) {
                personId = entity.person.id;
            }

            let person = await normalizeSerializedArray(personSerializedInArray, "Persons", personId);
            entity.person = person;
        }

        const action = props.insertingMode ? "Insert" : "Update";

        await fetch(`/Data/${action}${entityName}`, {
            method: 'POST',
            body: JSON.stringify(entity),
            headers: {
                'Content-Type': 'application/json'
            },
        });

        props.goToSection();
    }

    return (
        <button type="button" class="btn btn-success d-none" onClick={save} id="saveButton">{props.insertingMode ? "Добавить" : "Сохранить"}</button>
    );
}
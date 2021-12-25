import React, { useState } from 'react';
import Section from './Section';
import { Redirect } from 'react-router-dom';
import $ from 'jquery';

import * as UrlParser from './UrlParser';
import * as SchemaProvider from './Providers/SchemaProvider';
import * as EntityProvider from './Providers/EntityProvider';

export default function SaveButton() {
    const [routeToSection, setRouteToSection] = useState([]);

    async function normalizeSerializedArray(serializedArray, entityName, personId) {
        if (!serializedArray) {
            throw new Error("Serialized array is not defined");
        }

        SchemaProvider.validateEntityName(entityName);

        if ((entityName === "Persons") && (!personId)) {
            throw new Error("Entity name as Persons is defined, but person identifier is not defined");
        }

        let entitySchema = await SchemaProvider.getSchema(entityName);

        let entityId;

        if (entityName === "Persons")
            entityId = personId;
        else
            entityId = UrlParser.getEntityIdFromUrlForCardPage();

        let entity = await EntityProvider.getEntity(entityName, entityId);

        await entitySchema.forEach(column => {
            const dataBaseColumnName = column.dataBaseColumnName;
            const columnName = dataBaseColumnName[0].toLowerCase() + dataBaseColumnName.slice(1);

            const columnInSerializedArray = serializedArray.find(item => item.name === dataBaseColumnName);

            if (columnInSerializedArray) {
                const columnNameInSerializedArray = columnInSerializedArray.name;
                let columnValueInSerializedArray = columnInSerializedArray.value;

                if (!columnValueInSerializedArray) {
                    columnValueInSerializedArray = null;
                }

                const isDate = (columnValueInSerializedArray !== null)
                    && ((columnNameInSerializedArray.endsWith("Date")) || (columnNameInSerializedArray === "Birthday"));

                if (isDate) {
                    const splittedValue = columnValueInSerializedArray.split('/');
                    columnValueInSerializedArray = `${splittedValue[2]}-${splittedValue[1]}-${splittedValue[0]}`;
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

        let entityForUpdate = await normalizeSerializedArray(entitySerializedInArray, entityName);
        const personId = entityForUpdate.person.id;

        if (personSerializedInArray) {
            let personForUpdate = await normalizeSerializedArray(personSerializedInArray, "Persons", personId);
            entityForUpdate.person = personForUpdate;
        }

        await fetch(`/Data/Update${entityName}`, {
            method: 'POST',
            body: JSON.stringify(entityForUpdate),
            headers: {
                'Content-Type': 'application/json'
            },
        });

        const newRoute = <Redirect from={`/${entityName}CardPage`} to={`/${entityName}Section`} />;
        setRouteToSection(newRoute);
    }

    return (
        <di>
            <button type="button" class="btn btn-success d-none" onClick={save} id="saveButton">Сохранить</button>
            {routeToSection}
        </di>
    );
}
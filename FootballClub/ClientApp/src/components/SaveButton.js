import React from 'react';
import $ from 'jquery';

import * as UrlParser from './UrlParser';
import * as SchemaProvider from './Providers/SchemaProvider';
import * as EntityProvider from './Providers/EntityProvider';

export default function SaveButton(props) {

    function getDataFromForm(entityName, personId) {
        SchemaProvider.validateEntityName(entityName);

        const insertingMode = props.insertingMode;

        let entity = $(`#${entityName}-info-form`).serializeArray().reduce((obj, item) => {
            const itemName = item.name;

            const isNeedSetNull =
                (insertingMode) || ((itemName === "PersonId") && (!props.skipPersonId))

            if (isNeedSetNull)
                item.value = null;

            if (item.value === "false")
                item.value = false;
            else if (item.value === "true")
                item.value = true;

            obj[itemName] = item.value;
            return obj;
        }, {});

        if ((personId) && (!insertingMode))
            entity.personId = personId;

        return entity
    }

    async function save() {
        const entityName = UrlParser.getEntityNameFromUrlForCardPage();
        const entityId = UrlParser.getEntityIdFromUrlForCardPage();
        const insertingMode = props.insertingMode;

        let entityFromDb;

        if (!insertingMode)
            entityFromDb = await EntityProvider.getEntity(entityName, entityId);

        let entity = getDataFromForm(entityName, entityFromDb?.person?.id);
        let person = getDataFromForm('Persons');

        if (!insertingMode)
            entity.Id = entityId;

        if (Object.keys(person).length !== 0) {
            person.id = entityFromDb.person.id;
            entity.person = person;
        }

        const action = props.insertingMode ? "Insert" : "Update";

        await fetch(`/${entityName}/${action}Entity`, {
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
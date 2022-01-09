import React from 'react';
import $ from 'jquery';

import * as UrlParser from './UrlParser';
import * as SchemaProvider from './Providers/SchemaProvider';
import * as EntityProvider from './Providers/EntityProvider';

export default function SaveButton(props) {

    function getDataFromForm(entityName) {
        SchemaProvider.validateEntityName(entityName);

        let entity = $(`#${entityName}-info-form`).serializeArray().reduce((obj, item) => {
            if (item.value === "false")
                item.value = false;
            else if (item.value === "true")
                item.value = true;

            obj[item.name] = item.value;
            return obj;
        }, {});

        return entity
    }

    async function save() {
        const entityName = UrlParser.getEntityNameFromUrlForCardPage();
        const entityId = UrlParser.getEntityIdFromUrlForCardPage();

        let entity = getDataFromForm(entityName);
        let person = getDataFromForm('Persons');

        if (!props.insertingMode)
            entity.Id = entityId;

        if (Object.keys(person).length !== 0)
            entity.person = person;

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
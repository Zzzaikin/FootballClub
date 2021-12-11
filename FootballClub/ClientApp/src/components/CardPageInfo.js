import React from 'react';

export default function CardPageInfo() {
    function getEntityName() {
        return window.location.pathname.replace("CardPage", "").replace("/", "");
    }

    function getEntityId() {
        return window.location.search.replace("?id=", "");
    }

    async function getRuEntitySchema() {
        let entityName = getEntityName();
        let response = await fetch(`Data/GetRuEntitySchema?entityName=${entityName}`);

        return await response.json();
    }

    async function getEnEntitySchema() {
        let entityName = getEntityName();
        let response = await fetch(`Data/GetEnEntitySchema?entityName=${entityName}`);

        return await response.json();
    }

    async function getEntity(entityName) {
        let entityNameValue = entityName || getEntityName();
        let entityId = getEntityId();
        let response = await fetch(`Data/Get${entityNameValue}ById?id=${entityId}`);

        return await response.json();
    }

    async function getInputsWithValuesByObjectSchema() {
        let columnNames = await getRuEntitySchema();
        let enColumnNames = await getEnEntitySchema();

        let mappedColumnNames = enColumnNames.map(enColumnName => {
            var index = enColumnNames.indexOf(enColumnName);
            var ruLabels = columnNames[index];

            return { en: enColumnName, ru: ruLabels };
        });

        let entity = await getEntity();

        let inputs = mappedColumnNames.map(column => {
            return (
                <div className="module-info">
                    <div class="input-group input-group-lg">
                        <div class="input-group-prepend">
                            <span class="input-group-text" id="inputGroup-sizing-lg">Large</span>
                        </div>
                        <input type="text" class="form-control"
                            aria-label={column.ru} aria-describedby="inputGroup-sizing-sm"
                            value={entity[column.en]} />
                    </div>
                </div>
            );
        });

        return inputs;
    }

    return (
        <div>
            {getInputsWithValuesByObjectSchema()}
        </div>
    );
}
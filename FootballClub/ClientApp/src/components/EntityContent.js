import React, { useEffect, useState } from 'react';
import InputsCard from './Input/InputsCard';
import $ from 'jquery';

import * as SchemaProvider from './Providers/SchemaProvider';
import * as UrlParser from './UrlParser';
import * as InputsBuilder from './Input/InputsBuilder';

export default function EntityContent(props) {
    const [entityInputs, setEntityInputs] = useState([]);
    const [personInfo, setPersonInfo] = useState([]);

    useEffect(() => {
        async function setInputsByEntitySchema() {
            const schemaName = UrlParser.getEntityNameFromUrlForCardPage();
            const entitySchema = await SchemaProvider.getSchema(schemaName);
            setEntityInputsBySchema(entitySchema);
        }

        setInputsByEntitySchema();
    }, []);

    async function getPersonSchema() {
        return await SchemaProvider.getSchema("Persons");
    }

    async function setEntityInputsBySchema(entitySchema) {
        InputsBuilder.validateSchema(entitySchema);

        let entity;
        let person;

        if (!props.insertingMode) {
            entity = await InputsBuilder.getEntity();
            const personId = entity?.personId;

            if (personId) {
                if ((entity.whetherToLoadPerson)) {
                    person = entity.person
                }
            }
        }

        const skipPersonId = props.skipPersonId;

        let mappedEntityInputs = InputsBuilder.getMappedInputsBySchema(entitySchema, entity, skipPersonId, showSaveButtton);
        setEntityInputs(mappedEntityInputs);

        const entityName = UrlParser.getEntityNameFromUrlForCardPage();

        const whetherPersonInfo = (entityName === "Players") || (entityName === "Coaches") || (entityName === "PlayerManagers");

        if (whetherPersonInfo) {
            let personSchema = await getPersonSchema();
            let mappedPersonInputs = InputsBuilder.getMappedInputsBySchema(personSchema, person, skipPersonId, showSaveButtton);

            let personInfoCard = getPersonInfoCard(mappedPersonInputs);
            setPersonInfo(personInfoCard);
        }
    }

    function showSaveButtton() {
        $('#saveButton').removeClass('d-none');
        $('#canselButton').addClass('cancel-button');
    }

    function getPersonInfoCard(mappedPersonInputs) {
        if (!mappedPersonInputs) {
            throw new Error("Person inputs is not defined");
        }

        return (
            <InputsCard
                header={"Персональная информация"}
                inputs={mappedPersonInputs}
            />
        );
    }

    return (
        <div className="inputs-container">
            <form id="Persons-info-form">
                {personInfo}
            </form>
            <form id={`${UrlParser.getEntityNameFromUrlForCardPage()}-info-form`}>
                <InputsCard
                    header={"Основная информация"}
                    inputs={entityInputs}
                />
            </form>
        </div>
    );
}
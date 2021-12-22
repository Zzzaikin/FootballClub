import React, { useEffect, useState } from 'react';
import InputMask from 'react-input-mask';
import Input from './Input/Input';
import InputsCard from './Input/InputsCard';
import CustomSelect from './CustomSelect';

import * as SchemaProvider from './Providers/SchemaProvider';
import * as EntityProvider from './Providers/EntityProvider';
import * as UrlParser from './UrlParser';

export default function EntityContentOnCardPage(props) {
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

    async function getEntity() {
        const entityName = UrlParser.getEntityNameFromUrlForCardPage();
        const entityId = UrlParser.getEntityIdFromUrlForCardPage();
        return await EntityProvider.getEntity(entityName, entityId);
    }

    async function setEntityInputsBySchema(entitySchema) {
        validateSchema(entitySchema);

        const entity = await getEntity();
        const personId = entity?.personId;
        let person;

        if (personId) {
            if ((entity.whetherToLoadPerson)) {
                let response = await fetch(`/Data/GetPersonsById?id=${personId}`);
                person = await response.json();
            }
        }

        let mappedEntityInputs = getMappedInputsBySchema(entitySchema, entity);
        setEntityInputs(mappedEntityInputs);

        if (person) {
            let personSchema = await getPersonSchema();
            let mappedPersonInputs = getMappedInputsBySchema(personSchema, person);

            let personInfoCard = getPersonInfoCard(mappedPersonInputs);
            setPersonInfo(personInfoCard);
        }
    }

    function getMappedInputsBySchema(schema, entity) {
        validateSchema(schema);

        if (!entity) {
            throw new Error("Entity is not defined");
        }
        let mappedInputs = schema.map(column => {

            const dataBaseColumnName = column.dataBaseColumnName;

            const skip = (dataBaseColumnName === "Id") || ((dataBaseColumnName === "PersonId") && (props.skipPersonId));

            if (skip)
                return;

            const columnName = dataBaseColumnName[0].toLowerCase() + dataBaseColumnName.slice(1);
            let inputComponent;

            if (dataBaseColumnName.includes("Phone")) {
                inputComponent =
                    <InputMask
                        mask="+\7(999)999 99 99"
                        maskChar=" " type="text"
                        className="form-control"
                        aria-describedby="inputGroup-sizing-sm"
                        defaultValue={entity[columnName]}
                        name={dataBaseColumnName} />;
            } else if (dataBaseColumnName.endsWith("Id")) {
                inputComponent =
                    <div type="text" className="input-group-prepend custom-input-group-prepend" name={dataBaseColumnName}>
                        <CustomSelect columnName={dataBaseColumnName} selected={entity[columnName]} />
                    </div>
            } else {
                inputComponent =
                    <input type="text" className="form-control" name={dataBaseColumnName}
                        aria-describedby="inputGroup-sizing-sm"
                        defaultValue={entity[columnName]} />;
            }

            return (
                <Input
                    columnName={column.dataBaseColumnName}
                    inputLabel={column.localizedColumnName}
                    inputComponent={inputComponent}
                />
            );
        });

        return mappedInputs;
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

    function validateSchema(schema) {
        if (!schema) {
            throw new Error("Schema can not be defined");
        }
    }

    return (
        <form className="inputs-container">
            {personInfo}
            <InputsCard
                header={"Основная информация"}
                inputs={entityInputs}
            />
        </form>
    );
}
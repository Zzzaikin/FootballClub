import InputMask from 'react-input-mask';
import Input from './Input';
import CustomSelect from '../CustomSelect';
import CardPageDatepicker from '../CardPageDatepicker';
import React from 'react';

import * as EntityProvider from '../Providers/EntityProvider';
import * as SchemaProvider from '../Providers/SchemaProvider';
import * as UrlParser from '../UrlParser';

export async function getMappedInputsBySchema(schema, entity, skipPersonId, onInputClick) {
    validateSchema(schema);

    let mappedInputs = await Promise.all(schema.map(async column => await getInputBySchema(column, entity, skipPersonId, onInputClick)));

    return mappedInputs;
}

export async function getEntity() {
    const entityName = UrlParser.getEntityNameFromUrlForCardPage();
    const entityId = UrlParser.getEntityIdFromUrlForCardPage();
    return await EntityProvider.getEntity(entityName, entityId);
}

export function validateSchema(schema) {
    if (!schema) {
        throw new Error("Schema can not be defined");
    }
}

async function getInputBySchema(column, entity, skipPersonId, onInputClick) {
    const dataBaseColumnName = column.columnName;
    const dataBaseDataType = column.dataType;

    const skip = (dataBaseColumnName === "Id") || ((dataBaseColumnName === "PersonId") && (skipPersonId));

    if (skip)
        return;

    const columnName = dataBaseColumnName[0].toLowerCase() + dataBaseColumnName.slice(1);
    const columnValue = entity ? entity[columnName] : "";
    let inputComponent;

    if (dataBaseColumnName.includes("Phone")) {
        inputComponent =
            <InputMask
                mask="+\7(999)999 99 99"
                maskChar=" " type="text"
                className="form-control"
                aria-describedby="inputGroup-sizing-sm"
                defaultValue={columnValue}
                name={dataBaseColumnName}
                onClick={onInputClick} />;
    } else if (dataBaseColumnName.endsWith("Id")) {
        let referencedEntityName = await SchemaProvider.getReferencedEntity(column.tableName, dataBaseColumnName);

        inputComponent =
            <div type="text" className="input-group-prepend custom-input-group-prepend" name={dataBaseColumnName}>
                <CustomSelect
                    columnName={dataBaseColumnName}
                    selected={columnValue}
                    onClick={onInputClick}
                    entityName={referencedEntityName}
                />
            </div>
    } else if (dataBaseDataType === "datetime") {
        if (columnValue) {
            const indexOfT = columnValue.indexOf('T');
            const date = columnValue.substr("T", indexOfT).split('-');

            inputComponent =
                <CardPageDatepicker name={dataBaseColumnName} date={new Date(date[0], date[1], date[2])} onClick={e => { onInputClick() }} />
        } else {
            inputComponent =
                <CardPageDatepicker name={dataBaseColumnName} onClick={onInputClick} />
        }
    } else if (dataBaseDataType === "tinyint") {
        let options;

        if (columnValue) {
            options =
                <>
                    <option selected value={true}>Да</option>
                    <option value={false}>Нет</option>
                </>
        } else {
            options =
                <>
                    <option value={true}>Да</option>
                    <option selected value={false}>Нет</option>
                </>
        }

        inputComponent =
            <div type="text" className="input-group-prepend custom-input-group-prepend">
                <select className="custom-select" name={dataBaseColumnName} onClick={onInputClick}>
                    {options}
                </select>
            </div>
    } else {
        inputComponent =
            <input type="text" className="form-control" name={dataBaseColumnName}
                aria-describedby="inputGroup-sizing-sm"
                defaultValue={columnValue}
                onClick={onInputClick} />;
    }

    return (
        <Input
            columnName={column.dataBaseColumnName}
            inputLabel={column.localizedColumnName}
            inputComponent={inputComponent}
        />
    );
}
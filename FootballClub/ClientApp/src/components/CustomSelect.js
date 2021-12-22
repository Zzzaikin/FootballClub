import React, { useEffect, useState } from 'react';

export default function CustomSelect(props) {
    const [options, setOptions] = useState();

    let from = 0;
    let count = 1000;

    useEffect(() => {
        setSelectOptions();
    }, []);

    async function setSelectOptions() {
        const columnName = props.columnName;

        if (!columnName) {
            throw new Error("Column name is not defined");
        }

        if (!columnName.endsWith("Id")) {
            throw new Error("This column is not lookup (does not end on \"Id\")");
        }

        const entityName = columnName.replace("Id", "");
        const selectedId = props.selected;

        let response = await fetch(`/Data/Get${entityName}Options?from=${from}&count=${count}`);
        let options = await response.json();

        const mappedOptions = options.map((option) => {
            return (
                <option defaultValue={option.id}>{option.displayValue}</option>
            );
        });

        const selected = options.find(option => option.id === selectedId);

        if (selected) {
            const selectedOption =
                <option selected value={selected.id}>{selected.displayValue}</option>;
            mappedOptions.push(selectedOption);
        }

        setOptions(mappedOptions);
    }

    return (
        <div type="text" className="input-group-prepend custom-input-group-prepend">
            <select className="custom-select" id="inputGroupSelect01" name={props.columnName}>
                <option value="">Выберите значение...</option>
                {options}
            </select>
        </div>
    );
}
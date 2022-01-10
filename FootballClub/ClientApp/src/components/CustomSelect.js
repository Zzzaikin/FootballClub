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

        const selectedId = props.selected;

        let response = await fetch(`/${props.entityName}/GetEntityOptions?from=${from}&count=${count}`);
        let options = await response.json();

        const mappedOptions = options.map((option) => {
            return (
                <option value={option.id}>{option.displayValue}</option>
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
            <select className="custom-select" name={props.columnName} onClick={props.onClick}>
                <option value="">Выберите значение...</option>
                {options}
            </select>
        </div>
    );
}
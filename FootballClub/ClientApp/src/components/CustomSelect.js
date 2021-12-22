import React, { useEffect, useState } from 'react';

export default function CustomOptions(props) {
    const [options, setOptions] = useState();

    let from = 0;
    let count = 15;

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
        let selectedParam = "";

        if (selectedId !== null)
            selectedParam = `selected=${selectedId}&`;

        let response = await fetch(`/Data/Get${entityName}Options?${selectedParam}from=${from}&count=${count}`);
        let options = await response.json();

        const mappedOptions = options.map((option) => {
            return (
                <option value={option.id}>{option.displayValue}</option>
            );
        });

        const selected = options.find(option => option.id === selectedId);

        const selectedOption =
            <option selected value={selected.id}>{selected.displayValue}</option>;
        mappedOptions.push(selectedOption);

        setOptions(mappedOptions);

        from += count;
    }

    return (
        <div type="text" className="input-group-prepend custom-input-group-prepend">
            <select className="custom-select" id="inputGroupSelect01">
                {options}
            </select>
        </div>
    );
}
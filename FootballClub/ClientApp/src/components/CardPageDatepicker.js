import React, { useState } from 'react';
import DatePicker, { registerLocale } from "react-datepicker";
import ru from "date-fns/locale/ru";

import "react-datepicker/dist/react-datepicker.css";

registerLocale("ru", ru);

export default function CustomDatepicker(props) {
    const [date, settDate] = useState(props.date);

    return (
        <div onClick={props.onClick}>
            <DatePicker className="react-datepicker-ignore-onclickoutside form-control custom-select"
                locale="ru"
                dateFormat="dd/MM/yyyy"
                selected={date}
                onChange={(date) => settDate(date)}
                name={props.name}
            />
        </div>
    );
}
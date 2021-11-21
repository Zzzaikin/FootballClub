import React, { useState}  from 'react';
import DatePicker, { registerLocale } from "react-datepicker";
import ru from "date-fns/locale/ru";

import "react-datepicker/dist/react-datepicker.css";

registerLocale("ru", ru);

export default function CustomDatepicker(props) {
    const [date, settDate] = useState(props.date);

    const CustomInput = React.forwardRef(({ value, onClick }, ref) => (
        <button className="btn btn-outline-info" onClick={onClick} ref={ref}>
            {value}
        </button>
    ));

    return (
        <div className="datepicker-with-label">
            <div>{props.label}</div>
            <DatePicker
                locale="ru"
                dateFormat="dd/MM/yyyy"
                selected={date}
                onChange={(date) => settDate(date)}
                customInput={<CustomInput />}
            />
        </div>    
    );
}
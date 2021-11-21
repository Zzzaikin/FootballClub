import React, { useState } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';
import CounterCard from './CounterCard';
import CustomDatepicker from './CustomDatepicker';

const data = [
    {
        name: 'Январь',
        uv: 4000,
        pv: 2400,
        amt: 2400,
    },
    {
        name: 'Февраль',
        uv: 3000,
        pv: 1398,
        amt: 2210,
    },
    {
        name: 'Март',
        uv: 2000,
        pv: 9800,
        amt: 2290,
    },
    {
        name: 'Апрель',
        uv: 2780,
        pv: 3908,
        amt: 2000,
    },
    {
        name: 'Май',
        uv: 1890,
        pv: 4800,
        amt: 2181,
    },
    {
        name: 'Июнь',
        uv: 2390,
        pv: 3800,
        amt: 2500,
    },
    {
        name: 'Июль',
        uv: 3490,
        pv: 4300,
        amt: 2100,
    },
    {
        name: 'Август',
        uv: 3490,
        pv: 4300,
        amt: 2100,
    },
    {
        name: 'Сентябрь',
        uv: 3490,
        pv: 4300,
        amt: 2100,
    },
    {
        name: 'Октябрь',
        uv: 3490,
        pv: 4300,
        amt: 2100,
    },
    {
        name: 'Ноябрь',
        uv: 3490,
        pv: 4300,
        amt: 2100,
    },
    {
        name: 'Декабрь',
        uv: 3490,
        pv: 4300,
        amt: 2100,
    }
];

export default function EmployeeRecoveriesMiniDashboard(props) {
    var startDate = new Date();
    var endDate = new Date();
    endDate.setMonth(startDate.getMonth() + 1);;

    return (
        <div className="mini-dashboard-container">
            <div className="datepickers-container">
                <CustomDatepicker label={"От"} date={startDate} />
                <CustomDatepicker label={"До"} date={endDate} />
                <button type="button" class="btn btn-info datepicker-button">Применить</button>
            </div>
            <div>
                <LineChart
                    width={900}
                    height={250}
                    data={data}
                    margin={{
                        top: 5,
                        right: 30,
                        left: 20,
                        bottom: 5,
                    }}
                >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Line type="monotone" dataKey="pv" stroke="#8884d8" activeDot={{ r: 8 }} />
                    <Line type="monotone" dataKey="uv" stroke="#82ca9d" />
                </LineChart>
            </div>
            <div className="counter-card-wrapper" >
                <CounterCard title="Сумма в руб." text="100 000" />
                <CounterCard title="Количество" text="27" />
            </div>
        </div>
    );
}
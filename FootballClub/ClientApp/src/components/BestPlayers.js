import React, { useState } from 'react';
import CustomDatepicker from './CardPageDatepicker';

export default function BestPlayers() {
    const [cards, setCards] = useState();

    function onShowButtonClick() {

    }

    return (
        <div className="best-players-wrapper">
            <div>
                <p>Турнир</p>
                <input type="text" className="form-control ml-3" name="tournament"
                    aria-describedby="inputGroup-sizing-sm"
                />
                <div className="ml-3 mt-5">
                    <p>Дата начала</p>
                    <CustomDatepicker name="StartDate"/>
                    <p>Дата окончания</p>
                    <CustomDatepicker name="EndDate"/>
                </div>
                <button type="button" class="btn btn-outline-info ml-3 mt-3" onClick={onShowButtonClick}>Показать</button>
            </div>
            <div className="ml-3">
                {cards}
            </div>
        </div>
    );
}
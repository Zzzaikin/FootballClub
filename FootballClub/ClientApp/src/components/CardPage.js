import React from 'react';

import '../styles/CardPage.css';

export default function CardPage(props) {


    return (
        <div className="card-main-container">
            <div className="card-top-container">
                <div className="buttons-container">
                    <button type="button" class="btn btn-success">Сохранить</button>
                    <button type="button" class="btn btn-primary cancel-button">Отмена</button>
                </div>
            </div>
            <div className="entity-content">
                {props.content}
            </div>            
        </div>
    );
}
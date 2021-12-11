import React from 'react';
import '../styles/CardPage.css';

export default function CardPage(props) {
    const defaultContent = () => {
        return (
            <div className="default-layout" >
                <div className="main-info">
                    {props.mainInfo}
                </div>
                <div className="additional">
                    {props.additional}
                </div>
            </div>
        );
    }

    function getContent() {
        if (!props.children)
            return defaultContent;

        return props.children;
    }

    return (
        <div className="card-main-container">
            <div className="card-top-container">
                <div className="buttons-container">
                    <button type="button" class="btn btn-success">Сохранить</button>
                    <button type="button" class="btn btn-primary cancel-button">Отмена</button>
                </div>
            </div>
            {getContent()}
            
        </div>
    );
}
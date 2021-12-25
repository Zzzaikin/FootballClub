import React from 'react';
import { Link } from 'react-router-dom';
import SaveButton from './SaveButton';

import * as UrlParser from './UrlParser';

import '../styles/CardPage.css';

export default function CardPage(props) {
    const entityName = UrlParser.getEntityNameFromUrlForCardPage();

    return (
        <div className="card-main-container">
            <div className="card-top-container">
                <div className="buttons-container">
                    <SaveButton />
                    <Link type="button" className="btn btn-primary" to={`/${entityName}Section`} id="canselButton" >
                        Отмена
                    </Link>
                </div>
            </div>
            <div className="entity-content">
                {props.content}
            </div>
        </div>
    );
}
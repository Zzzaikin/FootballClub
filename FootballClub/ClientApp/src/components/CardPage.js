import React, { useState } from 'react';
import { Redirect } from 'react-router-dom';
import SaveButton from './SaveButton';

import * as UrlParser from './UrlParser';

import '../styles/CardPage.css';

export default function CardPage(props) {
    const [redirectToSection, setRedirectToSection] = useState();

    const entityName = UrlParser.getEntityNameFromUrlForCardPage();

    function goToSection() {
        const redirectComponent = <Redirect from={`/${entityName}CardPage`} to={`/${entityName}Section`} />;
        setRedirectToSection(redirectComponent);
    }

    return (
        <div className="card-main-container">
            <div className="card-top-container">
                <div className="buttons-container">
                    <SaveButton goToSection={goToSection}/>
                    <button type="button" className="btn btn-primary" id="canselButton" onClick={goToSection}>Отмена</button>
                </div>
            </div>
            <div className="entity-content">
                {props.content}
            </div>
            {redirectToSection}
        </div>
    );
}
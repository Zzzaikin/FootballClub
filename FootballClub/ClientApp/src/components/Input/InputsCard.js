import React from 'react';

import '../../styles/CardPage.css';

export default function InputsCard(props) {
    return (
        <div className="card ml-0 card-with-inputs">
            <div className="card-body">
                <h5 className="card-title">{props.header}</h5>
                {props.inputs}
            </div>
        </div>
    );
}
import React from 'react';

export default function Input(props) {

    return (
        <div className="input-info">
            <div class="input-group input-group-lg">
                <div class="input-group-prepend">
                    <span
                        className="input-group-text input"
                        id="inputGroup-sizing-lg"
                        style={{ fontSize: 15 }}>{props.inputLabel}</span>
                </div>
                {props.inputComponent}
            </div>
        </div>
    );
}
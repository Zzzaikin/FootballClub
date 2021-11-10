import React, { useState } from 'react';
import { WRAPPER_REF } from './Layout'

function LowerButtons() {
    const [turnButtonCaption, setTurnButtonCaption] = useState(">");        

    function onTurnButtonClick(e) {
        const caption = turnButtonCaption === ">" ? "<" : ">";
        setTurnButtonCaption(caption);

        const wrapper = WRAPPER_REF.current;
        wrapper.classList.toggle('is-nav-open');
    }

    return (
        <div className="lowerButtons">
            <button type="button" className="btn btn-outline-dark"
                style={{ "marginRight": "90px" }}>Выход</button>
            <button type="button" className="btn btn-outline-info"
                style={{ "marginLeft": "50px" }} onClick={onTurnButtonClick}>{turnButtonCaption}</button>
        </div> 
    );
}

export default LowerButtons;
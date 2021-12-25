import React, { useEffect, useState } from 'react';
import { WRAPPER_REF } from './Layout';
import { SECTION_WRAPPER_REF, CARD_CONTAINER_REF } from './Section';

function LowerButtons() {
    const [turnButtonCaption, setTurnButtonCaption] = useState(">");

    useEffect(() => {
        setTimeout(onTurnButtonClick, 500);
    }, []);

    function onTurnButtonClick(e) {
        const caption = turnButtonCaption === ">" ? "<" : ">";
        setTurnButtonCaption(caption);

        const wrapperRefs = [WRAPPER_REF, SECTION_WRAPPER_REF, CARD_CONTAINER_REF];

        wrapperRefs.forEach(ref => {
            if (!ref)
                return;

            const wrapper = ref.current;

            if (!wrapper)
                return;

            wrapper.classList.toggle('is-nav-open');
        });
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
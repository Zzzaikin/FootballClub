import React from 'react';
import '../css/ModalBox.css';

export default function ModalBox({ active, setActive, setModalBoxInCard, children}) {
    return (
        <div className={active ? "modal-box active" : "modal-box"} onClick={() => setModalBoxInCard([])}>
            <div className={active ? "modal-box-content active" : "modal-box-content"} onClick={e => e.stopPropagation()}>
                {children}
            </div>
        </div>
    );
}

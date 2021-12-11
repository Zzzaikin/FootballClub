import React, { useEffect, useState } from 'react';
import '../styles/ModalBox.css';

export default function ModalBox({ setModalBoxInCard, children }) {
    const [isActive, setIsActive] = useState(false);

    useEffect(() => {
        setIsActive(true);
    });

    return (
        <div className={isActive ? "modal-box active" : "modal-box"} onClick={() => setModalBoxInCard([])}>
            <div className={isActive ? "modal-box-content active" : "modal-box-content"} onClick={e => e.stopPropagation()}>
                {children}
            </div>
        </div>
    );
}

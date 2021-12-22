import React, { useState } from 'react';
import ModalBox from './ModalBox';
import { Link } from 'react-router-dom';

function Card(props) {
    const [deleteModalBox, setDeleteModalBox] = useState([]);

    const modalBox = () => {
        return (
            <ModalBox setModalBoxInCard={setDeleteModalBox} >
                <p className="modal-box-text-body">Вы уверены, что хотите удалить запись?</p>
                <div className="buttons-wrapper">
                    <a class="btn btn-danger btn-card btn-card-deletebtn btn-white-text" onClick={() => deleteRow()}>Удалить</a>
                    <a class="btn btn-primary btn-card btn-white-text" onClick={() => setDeleteModalBox([])}>Отмена</a>
                </div>
            </ModalBox>
        );
    }

    let entityId = props.entityId;

    async function deleteRow() {
        let entityName = props.entityName.toLowerCase();
        let response = await fetch(`/Data/DeleteEntity?entityName=${entityName}&id=${entityId}`, {
            method: 'DELETE'
        });

        setDeleteModalBox([]);

        if (response.ok)
            props.setCardsInSection(props.entityName);
    }

    return (
        <div class="card">
            <div class="card-body">
                {props.header}
                {props.firstParagraph}
                {props.secondParagraph}
                {props.thirdParagraph}
                <div className="buttons-wrapper">
                    <Link className="btn btn-primary btn-card" to={`/${props.entityName}CardPage?id=${props.entityId}`} >
                        Открыть
                    </Link>
                    <a class="btn btn-danger btn-card btn-card-deletebtn" onClick={() => setDeleteModalBox(modalBox())}>Удалить</a>
                    {props.dateParagraph}
                </div>
            </div>
            {deleteModalBox}
        </div>
    );
}

export default Card;
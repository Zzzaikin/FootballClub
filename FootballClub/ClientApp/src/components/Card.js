import React from 'react';

function Card(props) {
    return (
        <div class="card">
            <div class="card-body">
                {props.header}
                {props.firstParagraph}
                {props.secondParagraph}
                {props.thirdParagraph}
                <div className="buttons-wrapper">
                    <a href="#" class="btn btn-primary btn-card">Открыть</a>
                    <a href="#" class="btn btn-danger btn-card btn-card-deletebtn">Удалить</a>
                    {props.dateParagraph}
                </div>
            </div>
        </div>
    );
}

export default Card;
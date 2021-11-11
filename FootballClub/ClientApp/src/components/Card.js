import React from 'react';

function Card(props) {
    return (
        <div class="card">
            <div class="card-body">
                {props.header}
                {props.firstParagraph}
                {props.secondParagraph}
                {props.thirdParagraph}
                <div className="buttonWrapper">
                    <a href="#" class="btn btn-primary">Открыть</a>
                    {props.dateParagraph}
                </div>
            </div>
        </div>
    );
}

export default Card;
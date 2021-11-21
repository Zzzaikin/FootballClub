import React from 'react';

export default function CounterCard(props) {
    return (
        <div class="card text-white bg-info mb-3 counter-card" style={{ "width": "135px", "height": "80px" }}>
            <div class="card-header counter-card-header">{props.title}</div>
            <div class="card-body counter-card-body">
                <h5 class="card-title" style={{ "font-size": "14px" }}>{props.text}</h5>
            </div>
        </div>    
    );
}
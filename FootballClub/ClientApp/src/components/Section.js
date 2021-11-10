import React from 'react';

function Section(props) {
    async function getCards() {
        let path = props.match.path;
        const entityName = path.slice(1).split("S")[0];

        let response = await fetch(`Get${entityName}ForSection`);
        let data = await response.json();

        let cards = data.map(item => {
            return (
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">{item.person.name}</h5>
                        <p class="card-text">{`${item.homePhoneNumber} ${item.workPhoneNumber}`}</p>
                        <p class="card-text">{item.address}</p>
                        <a href="#" class="btn btn-primary">Go somewhere</a>
                    </div>
                </div>   
            );
        });

        return (
            <div class="tab-content" id="v-pills-tabContent">
                {cards}
            </div>
        );
    }

    return (
        <div class="sectionWrapper">
            {getCards()}
        </div>
    );
}

export default Section;
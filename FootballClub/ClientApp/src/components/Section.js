import React, { useState } from 'react';
import { Component } from 'react';

export let SECTION_WRAPPER_REF;

class Section extends Component {
    constructor(props) {
        super(props);
        this.state = {
            cards: [],
            sectionWrapperRef: React.createRef()
        }

        SECTION_WRAPPER_REF = this.state.sectionWrapperRef;
    }

    componentDidMount = () => {
        this.setCards(); 
    }

    async setCards() {
        let path = this.props.match.path;
        const entityName = path.slice(1).split("S")[0];


        let response = await fetch(`Data/Get${entityName}ForSection`);
        let result = await response.json();

        let cardsMarkup = result?.map(cardData => {
            return (<div class="card">
                <div class="card-body">
                    <h5 class="card-title">{cardData.person.name}</h5>
                    <p class="card-text">{`Рабочий телефон: ${cardData.person.workPhoneNumber}`}</p>
                    <p class="card-text">{`Домашний телефон: ${cardData.person.homePhoneNumber}`}</p>
                    <p class="card-text">{`Адрес: ${cardData.person.address}`}</p>
                    <a href="#" class="btn btn-primary">Go somewhere</a>
                </div>
            </div>);
        });

        this.setState(state => {
            return { cards: cardsMarkup };
        });
    }

    render() {
        return (
            <div ref={SECTION_WRAPPER_REF} class="sectionWrapper is-nav-open">
                {this.state.cards}
            </div>
        );
    };
}

export default Section;
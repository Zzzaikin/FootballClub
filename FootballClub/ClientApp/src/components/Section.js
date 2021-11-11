import React, { useState, Component } from 'react';
import Card from './Card';

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
            let cardHeader = <h5 class="card-title">{cardData.person?.name}</h5>;
            let firstP;
            let secondP;
            let thirdP;
            let date;

            if ((entityName === "EmployeeRecoveries") || (entityName === "Matches")) {
                let dateValue = cardData.date.split("T")[0];

                firstP = <p class="card-text">{`Рабочий телефон: ${cardData.person?.workPhoneNumber}`}</p>;
                secondP = <p class="card-text">{`Причина взыскания: ${cardData.recoveryReason?.name}`}</p>;
                thirdP = <p class="card-text">{`Сумма: ${cardData.sum}`}</p>;
                date = <p class="card-text smallFont">{`Дата: ${dateValue}`}</p>;
            } else {
                firstP = <p class="card-text">{`Рабочий телефон: ${cardData.person?.workPhoneNumber}`}</p>;
                secondP = <p class="card-text">{`Домашний телефон: ${cardData.person?.homePhoneNumber}`}</p>;
                thirdP = <p class="card-text">{`Адрес: ${cardData.person?.address}`}</p>;
            }

            return (
                <Card header={cardHeader} firstParagraph={firstP}
                    secondParagraph={secondP} thirdParagraph={thirdP}
                    dateParagraph={date} />
            );
        });

        this.setState(state => {
            return { cards: cardsMarkup };
        });
    }

    render() {
        return (
            <div ref={SECTION_WRAPPER_REF} class="sectionWrapper is-nav-open">
                {this.props.miniDashboard}
                {this.state.cards}
            </div>
        );
    };
}

export default Section;
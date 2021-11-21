import React, { Component } from 'react';
import Card from './Card';

export let SECTION_WRAPPER_REF;
export let CARD_CONTAINER_REF;

class Section extends Component {
    constructor(props) {
        super(props);

        this.state = {
            cards: [],
            sectionWrapperRef: React.createRef(),
            cardContainerRef: React.createRef()
        };

        SECTION_WRAPPER_REF = this.state.sectionWrapperRef;
        CARD_CONTAINER_REF = this.state.cardContainerRef;
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
            let dateValue;

            switch (entityName) {
                case "EmployeeRecoveries":
                    dateValue = cardData.date.split("T")[0];
                    firstP = <p class="card-text">{`Рабочий телефон: ${cardData.person?.workPhoneNumber}`}</p>;
                    secondP = <p class="card-text">{`Причина взыскания: ${cardData.recoveryReason?.name}`}</p>;
                    thirdP = <p class="card-text">{`Сумма: ${cardData.sum}`}</p>;
                    date = <p class="card-text card-date">{`Дата: ${dateValue}`}</p>;
                    break;

                case "Matches":
                    dateValue = cardData.date.split("T")[0];
                    cardHeader = <h5 class="card-title">{`${cardData.ourTeamName} - ${cardData.enemyTeamName}`}</h5>;
                    firstP = <p class="card-text">{`Счёт: ${cardData.ourTeamScores} - ${cardData.enemyTeamScores}`}</p>;
                    secondP = <p class="card-text">{`Длительность: ${cardData.duration}`}</p>;
                    date = <p class="card-text card-date">{`Дата: ${dateValue}`}</p>;
                    break;

                case "Disqualifications":
                    cardHeader = <h5 class="card-title">{`${cardData.player?.person?.name}`}</h5>;
                    firstP = <p class="card-text">{`Причина: ${cardData.name}`}</p>;
                    secondP = <p class="card-text">{`Начало: ${cardData.startDate.split("T")[0]}`}</p>;
                    thirdP = <p class="card-text">{`Начало: ${cardData.endDate.split("T")[0]}`}</p>;
                    break;

                default:
                    firstP = <p class="card-text">{`Рабочий телефон: ${cardData.person?.workPhoneNumber}`}</p>;
                    secondP = <p class="card-text">{`Домашний телефон: ${cardData.person?.homePhoneNumber}`}</p>;
                    thirdP = <p class="card-text">{`Адрес: ${cardData.person?.address}`}</p>;
                    break;
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
                <div ref={CARD_CONTAINER_REF} className="cards-container is-nav-open" >
                    {this.state.cards}
                </div>
            </div>
        );
    };
}

export default Section;
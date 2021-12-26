import React, { Component } from 'react';
import Card from './Card';
import $ from 'jquery';
import { Link } from 'react-router-dom';

export let SECTION_WRAPPER_REF;
export let CARD_CONTAINER_REF;

class Section extends Component {
    constructor(props) {
        super(props);

        this.state = {
            cards: [],
            isNavOpen: ""
        };

        SECTION_WRAPPER_REF = React.createRef();
        CARD_CONTAINER_REF = React.createRef();
    }

    componentDidMount = () => {
        const tabContent = $(".wrapper");
        const isNavOpenState = tabContent[0].className.endsWith("is-nav-open") ? "is-nav-open" : "";
        this.setState(state => {
            return { isNavOpen: isNavOpenState };
        });

        this.setCards();
    }

    getEntityName() {
        return this.props.match.path.slice(1).split("S")[0];
    }

    async setCards(entityName) {
        const name = entityName || this.getEntityName();

        let response = await fetch(`Data/Get${name}`);
        let result = await response.json();

        let cardsMarkup = result?.map(cardData => {
            let cardHeader = <h5 class="card-title">{cardData.person?.name}</h5>;
            let firstP;
            let secondP;
            let thirdP;
            let date;
            let dateValue;

            switch (name) {
                case "EmployeeRecoveries":
                    dateValue = cardData.date.split("T")[0];
                    firstP = <p class="card-text">{`Рабочий телефон: ${cardData.person?.workPhoneNumber}`}</p>;
                    secondP = <p class="card-text">{`Причина взыскания: ${cardData.recoveryReason?.displayName}`}</p>;
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
                    cardHeader = <h5 class="card-title">{`${cardData.person?.name}`}</h5>;
                    firstP = <p class="card-text">{`Причина: ${cardData.displayName}`}</p>;
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
                    dateParagraph={date} entityId={cardData.id}
                    entityName={name} setCardsInSection={this.setCards.bind(this)} />
            );
        });

        this.setState(state => {
            return { cards: cardsMarkup };
        });
    }

    render() {
        return (
            <div ref={SECTION_WRAPPER_REF} class={`sectionWrapper ${this.state.isNavOpen}`}>
                {this.props.miniDashboard}
                <div ref={CARD_CONTAINER_REF} className={`cards-container ${this.state.isNavOpen}`} >
                    <div className="top-container" >
                        <Link type="button" class="btn btn-link add-button" to={`Insert${this.getEntityName()}`}>Добавить...</Link>
                    </div>
                    {this.state.cards}
                </div>
            </div>
        );
    };
}

export default Section;
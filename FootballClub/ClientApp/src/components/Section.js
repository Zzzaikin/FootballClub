import React, { useState, useEffect } from 'react';
import Card from './Card';
import $ from 'jquery';
import { Link } from 'react-router-dom';

import '../styles/Section.css';

export let SECTION_WRAPPER_REF;
export let CARD_CONTAINER_REF;

export default function Section(props) {
    const [cards, setCards] = useState([]);
    const [isNavOpen, setIsNavOpen] = useState("");

    useEffect(() => {
        SECTION_WRAPPER_REF = React.createRef();
        CARD_CONTAINER_REF = React.createRef();

        const tabContent = $(".wrapper");
        const isNavOpenState = tabContent[0].className.endsWith("is-nav-open") ? "is-nav-open" : "";
        setIsNavOpen(isNavOpenState);

        setEntityCards();
    }, []);

    function getEntityName() {
        return props.match.path.slice(1).split("S")[0];
    }

    async function setEntityCards(entityName) {
        const name = entityName || getEntityName();

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
                    entityName={name} setCardsInSection={setEntityCards} />
            );
        });

        setCards(cardsMarkup);
    }

    return (
        <div ref={SECTION_WRAPPER_REF} class={`sectionWrapper ${isNavOpen}`}>
            {props.miniDashboard}
            <div ref={CARD_CONTAINER_REF} className={`cards-container ${isNavOpen}`} >
                <div className="top-container" >
                    <Link type="button" class="btn btn-link add-button" to={`Insert${getEntityName()}`}>Добавить запись...</Link>
                </div>
                {cards}
                <Link type="button" class="btn btn-link show-more-button">Показать ещё...</Link>
            </div>
        </div>
    );
};
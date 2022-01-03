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
    const [showMoreButton, setShowMoreButton] = useState();

    const from = 0;
    const countIncrement = 9;
    let count = 9;

    useEffect(() => {
        SECTION_WRAPPER_REF = React.createRef();
        CARD_CONTAINER_REF = React.createRef();

        const tabContent = $(".wrapper");
        const isNavOpenState = tabContent[0].className.endsWith("is-nav-open") ? "is-nav-open" : "";
        setIsNavOpen(isNavOpenState);

        setEntityCards();
        setShowMoreButtonVisible();
    }, []);

    function getEntityName() {
        return props.match.path.slice(1).split("S")[0];
    }

    async function setShowMoreButtonVisible() {
        const entityName = getEntityName();

        let response = await fetch(`${entityName}/GetCountOfEntityRecords`);
        let countOfRecords = await response.json();

        if ((countOfRecords - count) > 0) {
            const showMore =
                <li type="button" class="btn btn-link show-more-button" onClick={setMoreEntityCards}>Показать ещё...</li>;

            setShowMoreButton(showMore);
        } else {
            setShowMoreButton();
        }
    }

    function setMoreEntityCards() {
        count += countIncrement;
        setEntityCards();

        setShowMoreButtonVisible();
    }

    async function setEntityCards(entityName) {
        const name = entityName || getEntityName();

        let response = await fetch(`${name}/GetEntities?from=${from}&count=${count}`);
        let result = await response.json();

        let cardsMarkup = result?.map(cardData => {
            const cardHeaderValue = cardData.person?.name || "Сотрудник не выбран";
            let cardHeader = <h5 class="card-title">{cardHeaderValue}</h5>;
            let firstP;
            let secondP;
            let thirdP;
            let date;
            let dateValue;

            switch (name) {
                case "EmployeeRecoveries":
                    dateValue = cardData.date.split("T")[0];
                    firstP = <p class="card-text">{`Рабочий телефон: ${cardData.person?.workPhoneNumber || "Не указан"}`}</p>;
                    secondP = <p class="card-text">{`Причина взыскания: ${cardData.recoveryReason?.displayName || "Не указан"}`}</p>;
                    thirdP = <p class="card-text">{`Сумма: ${cardData.sum || "Не указана"}`}</p>;
                    date = <p class="card-text card-date">{`Дата: ${dateValue || "Не указана"}`}</p>;
                    break;

                case "Matches":
                    dateValue = cardData.date.split("T")[0];
                    cardHeader =
                        <h5 class="card-title">
                            {`${cardData.ourTeamName || "Наша команда не указана"} - ${cardData.enemyTeamName || "Команда противника не указана"}`}
                        </h5>;

                    firstP =
                        <p class="card-text">
                            {`Счёт: ${cardData.ourTeamScores || "Голы нашей команды не указан"} - ${cardData.enemyTeamScores || "Голы команды противника не указан"}`}
                        </p>;

                    secondP = <p class="card-text">{`Длительность: ${cardData.duration || "Не указана"}`}</p>;
                    date = <p class="card-text card-date">{`Дата: ${dateValue || "Не указана"}`}</p>;
                    break;

                case "Disqualifications":
                    firstP = <p class="card-text">{`Причина: ${cardData.displayName || "Не указана"}`}</p>;
                    secondP = <p class="card-text">{`Начало: ${cardData.startDate?.split("T")[0] || "Не указана"}`}</p>;
                    thirdP = <p class="card-text">{`Начало: ${cardData.endDate?.split("T")[0] || "Не указана"}`}</p>;
                    break;

                default:
                    firstP = <p class="card-text">{`Рабочий телефон: ${cardData.person?.workPhoneNumber || "Не указана"}`}</p>;
                    secondP = <p class="card-text">{`Домашний телефон: ${cardData.person?.homePhoneNumber || "Не указана"}`}</p>;
                    thirdP = <p class="card-text">{`Адрес: ${cardData.person?.address || "Не указана"}`}</p>;
                    break;
            }

            return (
                <Card header={cardHeader} firstParagraph={firstP}
                    secondParagraph={secondP} thirdParagraph={thirdP}
                    dateParagraph={date} entityId={cardData.id}
                    entityName={name} setCardsInSection={setEntityCards} />
            );
        });

        setShowMoreButtonVisible();
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
                {showMoreButton}
            </div>
        </div>
    );
};
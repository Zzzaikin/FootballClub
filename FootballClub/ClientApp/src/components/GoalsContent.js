import React, { useState } from 'react';
import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import $ from 'jquery';

import * as SchemaProvider from './Providers/SchemaProvider';
import * as InputsBuilder from './Input/InputsBuilder';
import * as UrlParser from './UrlParser';

export default function GoalsContent() {
    const [ourTeamGoalsContent, setOurTeamGoalsContent] = useState();
    const [enemyTeamGoalsContent, setEnemyTeamGoalsContent] = useState();

    useEffect(() => {
        setGoalsContent('Our', setOurTeamGoalsContent);
        setGoalsContent('Enemy', setEnemyTeamGoalsContent);
    }, []);

    async function setGoalsContent(team, setContent) {
        if (!team) {
            throw new Error("Team is no defined");
        }

        const entityName = `${team}TeamGoals`;

        let schema = await SchemaProvider.getSchema(entityName);
        let goals = await getGoals(entityName);

        if (goals.length === 0)
            return;

        let inputsCardsCollection = await Promise.all(goals.map(async goal => {

            let inputs = await InputsBuilder.getMappedInputsBySchema(schema, goal, false, showSaveButtton);
            const inputsCard =
                <div className="card ml-0 card-with-inputs">
                    <div className="card-body">
                        {inputs}
                    </div>
                </div>
            return inputsCard;
        }));

        setContent(inputsCardsCollection);
    }

    function showSaveButtton() {
        $('#saveButton').removeClass('d-none');
        $('#canselButton').addClass('cancel-button');
    }

    async function getGoals(entityName) {
        SchemaProvider.validateEntityName(entityName);
        const matchId = UrlParser.getEntityIdFromUrlForCardPage();

        let response = await fetch(`/${entityName}/GetGoalsByMatchId?matchId=${matchId}`);
        return await response.json();
    }

    return (
        <div className="goals-content">
            <h5 className="card-title goals-title">Голы нашей команды</h5>
            <div className="team-goals-content">
                {ourTeamGoalsContent}
                <Link type="button" class="btn btn-link add-button">Добавить гол нашей команде...</Link>
            </div>
            <h5 className="card-title goals-title">Голы команды противника</h5>
            <div className="team-goals-content">
                {enemyTeamGoalsContent}
                <Link type="button" class="btn btn-link add-button">Добавить гол команде противника...</Link>
            </div>
        </div>
    );
}
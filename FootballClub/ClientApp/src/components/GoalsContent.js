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
        setOurTeamGoalsContent('Enemy', setEnemyTeamGoalsContent);
    }, []);

    async function setGoalsContent(team, setContent) {
        if (!team) {
            throw new Error("Team is no defined");
        }

        const entityName = `${team}TeamGoals`;

        let schema = await SchemaProvider.getSchema(entityName);
        let goals = await getGoals(entityName);
        let goalsInputs = [];

        if (goals.length === 0)
            return;

        Promise.all(goals.forEach(async goal => {
            let inputs = await InputsBuilder.getMappedInputsBySchema(schema, goal, false, showSaveButtton);
            goalsInputs.push(inputs);
        })).then(() => setContent(goalsInputs));
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
            <div className="our-goals-content">
                {ourTeamGoalsContent}
                <Link type="button" class="btn btn-link add-button">Добавить гол нашей команде...</Link>
            </div>
            <div className="enemy-goals-content">
                {enemyTeamGoalsContent}
                <Link type="button" class="btn btn-link add-button">Добавить гол команде противника...</Link>
            </div>
        </div>
    );
}
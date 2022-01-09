import React, { useState } from 'react';
import { useEffect } from 'react';
import { Link } from 'react-router-dom';

import * as SchemaProvider from './Providers/SchemaProvider';
import * as EntityProvider from './Providers/EntityProvider';

export default function GoalsContent() {
    const [ourTeamGoalsContent, setOurTeamGoalsContent] = useState();
    const [enemyTeamGoalsContent, setEnemyTeamGoalsContent] = useState();

    useEffect(() => {
        
    }, []);

    async function getGoalsContent(team) {
        if (!team) {
            throw new Error("Team is no defined");
        }

        const entityName = `${team}TeamGoals`;

        let schema = await SchemaProvider.getSchema(entityName);
        
    }

    return (
        <>
            <div className="our-goals-content">
                {ourTeamGoalsContent}
                <Link type="button" class="btn btn-link add-button">Добавить гол нашей команде...</Link>
            </div>
            <div className="enemy-goals-content">
                {enemyTeamGoalsContent}
                <Link type="button" class="btn btn-link add-button">Добавить гол команде противника...</Link>
            </div>
        </>
    );
}
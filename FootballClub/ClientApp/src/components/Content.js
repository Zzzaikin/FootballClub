import React from 'react';
import { Redirect, Route } from 'react-router-dom';
import CardPage from './CardPage';
import EmployeeRecoveriesMiniDashboard from './EmployeeRecoveriesMiniDashboard';
import MatchesMiniDashboard from './MatchesMiniDashboard';
import Section from './Section';
import EntityContent from './EntityContent';
import GoalsContent from './GoalsContent';

function Content() {
    return (
        <div class="tab-content" id="v-pills-tabContent">
            <Route path='/PlayersSection' component={Section} />
            <Route path='/CoachesSection' component={Section} />
            <Route path='/PlayerManagersSection' component={Section} />
            <Route path='/EmployeeRecoveriesSection' render={() =>
                <Section miniDashboard={<EmployeeRecoveriesMiniDashboard />} match={{ path: "/EmployeeRecoveriesSection"}} />} /> 
            <Route path='/MatchesSection' render={() =>
                <Section miniDashboard={<MatchesMiniDashboard />} match={{ path: "/MatchesSection"}} />} />
            <Route path='/DisqualificationsSection' component={Section} />

            <Route path='/PlayersCardPage' render={() => <CardPage content={<EntityContent skipPersonId={true} />} />} />
            <Route path='/CoachesCardPage' render={() => <CardPage content={<EntityContent skipPersonId={true}/>} />} />
            <Route path='/PlayerManagersCardPage' render={() => <CardPage content={<EntityContent skipPersonId={true}/>} />} />
            <Route path='/EmployeeRecoveriesCardPage' render={() => <CardPage content={<EntityContent skipPersonId={false} />} />} />
            <Route path='/MatchesCardPage' render={() =>
                <CardPage
                    content={<EntityContent skipPersonId={true} />}
                    goalsContent={<GoalsContent />}
                />}
            />
            <Route path='/DisqualificationsCardPage' render={() => <CardPage content={<EntityContent skipPersonId={false} />} />} />

            <Route path='/InsertPlayers' render={() => <CardPage content={<EntityContent skipPersonId={true} insertingMode={true} />} />} />
            <Route path='/InsertCoaches' render={() => <CardPage content={<EntityContent skipPersonId={true} insertingMode={true} />} />} />
            <Route path='/InsertPlayerManagers' render={() => <CardPage content={<EntityContent skipPersonId={true} insertingMode={true} />} />} />
            <Route path='/InsertEmployeeRecoveries' render={() => <CardPage content={<EntityContent skipPersonId={false} insertingMode={true} />} />} />
            <Route path='/InsertMatches' render={() => <CardPage content={<EntityContent skipPersonId={true} insertingMode={true} />} />} />
            <Route path='/InsertDisqualifications' render={() => <CardPage content={<EntityContent skipPersonId={false} insertingMode={true} />} />} />
            <Redirect from={"/"} to={"/PlayersSection"} />
        </div>
    );
}

export default Content;

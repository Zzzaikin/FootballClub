import React from 'react';
import CounterCard from './CounterCard';

export default function MatchesMiniDashboard() {
    return (
        <div className="matches-minidashboard-section">
            <CounterCard title="Побед" text="10" />
            <CounterCard title="Поражений" text="4" />
            <CounterCard title="Процент побед" text="71,43%" />
        </div>
    );
}
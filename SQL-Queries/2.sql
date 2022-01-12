select t.Id as tid, count(o.Id) + count(cop.cp) as c 
from footballclub.ourteamgoals as o, 
(
	select t2.Id as tid, count(o2.Id) as cp
    from footballclub.ourteamgoals as o2
    left join footballclub.matches as m2
	on o2.MatchId = m2.Id
	left join footballclub.tournaments as t2
	on m2.TournamentId = t2.Id
	left join footballclub.players as p2
	on o2.AuthorPlayerId = p2.Id
	left join footballclub.persons as per2 
	on p2.PersonId = per2.Id

where per2.Name = 'Ульянов Владимир Ильич'
group by t2.Id, o2.TouchdownPassPlayerId
) as cop

left join footballclub.matches as m
on o.MatchId = m.Id
left join footballclub.tournaments as t
on m.TournamentId = t.Id
left join footballclub.players as p
on o.AuthorPlayerId = p.Id
left join footballclub.persons as per 
on p.PersonId = per.Id

where per.Name = 'Ульянов Владимир Ильич'
group by t.Id, o.AuthorPlayerId

select go.cid, per.Name
from 
(
	select count(o.Id) as cid, o.AuthorPlayerId as aid from footballclub.ourteamgoals as o    
    left join footballclub.matches as m
    on o.MatchId = m.Id
    left join footballclub.tournaments as t
    on m.TournamentId = t.Id
    where 
		t.Name = 'Лига Европы'
        and t.StartDate = '2020-05-12'
        and t.EndDate = '2021-05-04'
	group by o.AuthorPlayerId
    
    order by cid desc
	limit 3
) as go

left join footballclub.players as p
on go.aid = p.Id

left join footballclub.persons as per 
on p.PersonId = per.Id
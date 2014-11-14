select db.Caption as divisionname, cb.Caption as categoryname from Dance.Division d inner join dance.BaseObject db on d.Id = db.Id
inner join Dance.Category c on c.DivisionId = d.Id 
inner join Dance.BaseObject cb on cb.Id = c.Id
order by db.Caption, cb.Caption
CREATE DEFINER=`root`@`localhost` FUNCTION `get_animal_id_by_visit_id`(in_id_visit int(10)) RETURNS int
    DETERMINISTIC
BEGIN
    SELECT animal_id
    INTO @animal_id FROM petadmin.visits
    WHERE visit_id = in_id_visit;
    RETURN @animal_id;
END
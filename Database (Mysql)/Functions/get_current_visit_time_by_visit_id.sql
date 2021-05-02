CREATE DEFINER=`root`@`localhost` FUNCTION `get_current_visit_time_by_visit_id`(in_id_visit int(10)) RETURNS datetime
    DETERMINISTIC
BEGIN
    SELECT visit_time
    INTO @current_visit_time FROM petadmin.visits
    WHERE visit_id = in_id_visit;
    RETURN @current_visit_time;
END
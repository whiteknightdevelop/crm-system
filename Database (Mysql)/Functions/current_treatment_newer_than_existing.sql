CREATE DEFINER=`root`@`localhost` FUNCTION `current_treatment_newer_than_existing`(in_id_treatment int(10), in_animal_id INT, in_current_visit_time DATETIME) RETURNS tinyint(1)
    DETERMINISTIC
BEGIN
    SET @is_not_deleted = 0;
    IF EXISTS(SELECT * FROM
        (SELECT animal_id, id_visit_ref, id_treatment_ref, visits_treatments_sent, visits_treatments_is_reminder_deleted, treatment_user_id, is_preventive_treatment_deleted
        FROM
            (SELECT * FROM petadmin.visits_treatments) AS visits_treatments
            RIGHT JOIN
            (SELECT * FROM petadmin.visits WHERE animal_id=in_animal_id) AS visits
            ON visit_id=id_visit_ref) AS visits_treatments_visits
        WHERE (id_treatment_ref=in_id_treatment) AND (visits_treatments_is_reminder_deleted=@is_not_deleted))
    THEN
        RETURN TRUE;
    ELSE
    RETURN FALSE;
    END IF;
END
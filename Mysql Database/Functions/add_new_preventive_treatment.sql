CREATE DEFINER=`root`@`localhost` FUNCTION `add_new_preventive_treatment`(in_id_visit int(10), in_id_treatment int(10), in_treatment_user_id int(10)) RETURNS int
    DETERMINISTIC
BEGIN
    SET @is_not_deleted = 0;
    INSERT INTO petadmin.visits_treatments (id_visit_ref, id_treatment_ref, treatment_user_id, visits_treatments_is_reminder_deleted)
    values (in_id_visit, in_id_treatment, in_treatment_user_id, @is_not_deleted);
    RETURN in_id_treatment;
END
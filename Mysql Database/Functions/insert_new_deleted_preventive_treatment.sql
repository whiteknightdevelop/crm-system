CREATE DEFINER=`root`@`localhost` FUNCTION `insert_new_deleted_preventive_treatment`(in_id_visit int(10), in_id_treatment int(10), in_treatment_user_id int(10)) RETURNS int
    DETERMINISTIC
BEGIN
    SET @is_deleted = 1;
    INSERT INTO petadmin.visits_treatments (id_visit_ref, id_treatment_ref, treatment_user_id, visits_treatments_is_reminder_deleted)
    values (in_id_visit, in_id_treatment, in_treatment_user_id, @is_deleted);
    RETURN in_id_treatment;
END
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_preventive_treatment`(IN in_id_visit_ref int(10), IN in_id_treatment_ref int(10), IN in_treatment_user_id int(10))
BEGIN
    DECLARE l_isodd INT;
    SET @is_not_deleted = 0;
    SET @is_deleted = 1;
    SET @animal_id = get_animal_id_by_visit_id(in_id_visit_ref);
    SET @current_visit_time = get_current_visit_time_by_visit_id(in_id_visit_ref);

    IF (if_treatment_exists_for_animal(in_id_visit_ref, in_id_treatment_ref))
    THEN
        IF (if_not_deleted_treatment_exists_for_animal(in_id_visit_ref, in_id_treatment_ref))
        THEN
            IF (if_current_treatment_newer_than_all_existing(in_id_treatment_ref, @animal_id, @current_visit_time))
            THEN
                SET l_isodd=update_old_treatment_reminders_as_deleted(@animal_id, in_id_visit_ref, in_id_treatment_ref, @current_visit_time);
                SET l_isodd= insert_new_preventive_treatment(in_id_visit_ref, in_id_treatment_ref, in_treatment_user_id);
            ELSE
                SET l_isodd= insert_new_deleted_preventive_treatment(in_id_visit_ref, in_id_treatment_ref, in_treatment_user_id);
            END IF;
        ELSE
            IF (if_current_treatment_newer_than_existing(in_id_treatment_ref, @animal_id, @current_visit_time))
            THEN
                SET l_isodd= insert_new_preventive_treatment(in_id_visit_ref, in_id_treatment_ref, in_treatment_user_id);
            ELSE
                SET l_isodd= insert_new_deleted_preventive_treatment(in_id_visit_ref, in_id_treatment_ref, in_treatment_user_id);
            END IF;
        END IF;
    ELSE
        SET l_isodd= insert_new_preventive_treatment(in_id_visit_ref, in_id_treatment_ref, in_treatment_user_id);
    END IF;
END
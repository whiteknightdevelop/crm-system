CREATE DEFINER=`root`@`localhost` PROCEDURE `archive_owner`(IN id_num int(10))
BEGIN
    SET @deleted = 1;

    /* Archive owner */
    UPDATE petadmin.owners
    SET owner_archive = @deleted
    WHERE owner_id = id_num;

    /* Archive animals */
    UPDATE petadmin.animals
    SET animal_archive = @deleted
    WHERE owner_id = id_num;

    /* Archive animals visits */
    UPDATE petadmin.visits
    RIGHT JOIN petadmin.animals
    ON petadmin.animals.animal_id = petadmin.visits.animal_id
    RIGHT JOIN petadmin.owners
    ON petadmin.owners.owner_id = petadmin.animals.owner_id
    SET
        petadmin.visits.visit_archive = @deleted,
        petadmin.animals.animal_archive = @deleted
    WHERE petadmin.owners.owner_id=id_num AND petadmin.animals.owner_id=id_num;

    /* Archive preventive tretments */
    UPDATE petadmin.visits_treatments
    RIGHT JOIN petadmin.visits
    ON petadmin.visits.visit_id = petadmin.visits_treatments.id_visit_ref
    RIGHT JOIN petadmin.animals
    ON petadmin.animals.animal_id = petadmin.visits.animal_id
    RIGHT JOIN petadmin.owners
    ON petadmin.owners.owner_id = petadmin.animals.owner_id
    SET
        petadmin.visits_treatments.visits_treatments_is_reminder_deleted = @deleted,
        petadmin.visits_treatments.is_preventive_treatment_deleted = @deleted
    WHERE petadmin.owners.owner_id=id_num AND petadmin.animals.owner_id=id_num;

    /* Delete reminders */
    DELETE petadmin.reminders
    FROM petadmin.reminders
    RIGHT JOIN petadmin.animals
    ON petadmin.animals.animal_id = petadmin.reminders.animal_id_reminder
    RIGHT JOIN petadmin.owners
    ON petadmin.owners.owner_id = petadmin.animals.owner_id
    WHERE petadmin.owners.owner_id=id_num AND petadmin.animals.owner_id=id_num;

    SELECT 'true' as response;
END
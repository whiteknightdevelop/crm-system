CREATE DEFINER=`root`@`localhost` PROCEDURE `get_followups_list_by_date_interval`(IN in_from_date DATE, IN in_to_date DATE)
BEGIN
    SELECT followup_id, owner_id_followup, date_followup, owner_details_followup, animal_name_followup, cause_followup, owner_archive
    FROM
        (SELECT * FROM petadmin.followups
        WHERE CAST(date_followup AS DATE) BETWEEN in_from_date AND in_to_date
        ORDER BY date_followup ASC) AS followupsByDate
    Left JOIN
        (SELECT * FROM petadmin.owners) AS owners
    ON owners.owner_id = followupsByDate.owner_id_followup
    WHERE owner_archive = 0
    ORDER BY date_followup DESC;
END
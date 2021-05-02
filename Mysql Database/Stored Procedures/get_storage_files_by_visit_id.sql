CREATE DEFINER=`root`@`localhost` PROCEDURE `get_storage_files_by_visit_id`(IN in_id_num int(10))
BEGIN
    SELECT table1.file_id, file_created, file_name, file_path, file_size, file_exists, file_extension, file_hash FROM
        (SELECT * FROM petadmin.visit_files 
        WHERE visit_id = in_id_num) AS table1
    LEFT JOIN petadmin.file_storage ON table1.file_id = petadmin.file_storage.file_id
    ORDER BY file_created DESC;
END
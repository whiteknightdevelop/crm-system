CREATE DEFINER=`root`@`localhost` PROCEDURE `update_storage_file`(
IN in_file_id INT(10),
IN in_file_created DATETIME,
IN in_file_name VARCHAR(128),
IN in_file_path VARCHAR(255),
IN in_file_size BIGINT,
IN in_file_exists TINYINT(1),
IN in_file_extension VARCHAR(45),
IN in_file_hash LONGTEXT)

BEGIN
    UPDATE petadmin.file_storage
    SET 
        file_id = in_file_id,
        file_created = in_file_created,
        file_name = in_file_name,
        file_path = in_file_path,
        file_size = in_file_size,
        file_exists = in_file_exists,
        file_extension = in_file_extension,
        file_hash = in_file_hash
    WHERE file_id = in_file_id;
END
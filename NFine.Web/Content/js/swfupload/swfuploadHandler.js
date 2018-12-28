/* ******************************************
*	初始化SWFUpload上传控件
*   上传单文件， 只要图片
* ****************************************** */
function InitSWFUpload(upurl, uppath, upsize, flashurl) {
    var sendUrl = upurl + "?action=MultipleFile&UpFilePath=" + uppath;
    var swfu = new SWFUpload({
        // Backend Settings
        upload_url: sendUrl,
        file_post_name: uppath,
        post_params: { "ASPSESSID": "NONE" },

        file_size_limit: upsize, // 2MB
        file_types: "*.jpg;*.jpge;*.png;*.gif",
        file_types_description: "JPG Images",
        file_upload_limit: "1",
       // file_queue_error_handler: fileQueueError,
       // file_dialog_complete_handler: fileDialogComplete,
       // upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: uploadSuccess,
        upload_complete_handler: uploadComplete,

        // Button Settings
        button_placeholder_id: "upload",
        button_width: 50,
        button_height: 23,
        button_text: '<span class="btn btn-default">选择文件</span>',//按钮文字
        button_window_mode: SWFUpload.WINDOW_MODE.TRANSPARENT,
        button_cursor: SWFUpload.CURSOR.HAND,

        // Flash Settings
        flash_url: flashurl,

        custom_settings: {
            upload_target: "show"
        },
        // Debug Settings
        debug: false
    });

}
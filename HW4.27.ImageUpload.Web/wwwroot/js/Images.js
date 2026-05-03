
$(() => {

    $.get('home/getlikedimageids').done(function (likedIds) {
        $('button[data-image-id]').each(function () {
            const id = $(this).data(image - id);
            if (id && likedIds.includes(id)) {
                $(this).prop('disabled', true)
            }
        })
    })

    $(document).on("click", "#likeimage", function () {
        const btn = $(this);
        const imageId = btn.data('image-id');

        btn.prop("disabled", true)

        $.post(`/home/addlike?imageid=${imageId}`)
            .done(function () {
                $.get(`/home/getlikescount?imageId=${imageId}`)
                    .done(function (likes) {
                        $(`#likecount-${imageId}`).text(likes);
                    })
            });
    });
    //how do i get this to happen for each image on the page, not just 1?


    setInterval(function () {
        $('button[data-image-id').each(function () {
            const id = $(this).data('image-id');
            $.get(`/home/getlikescount?imageid=${id}`)
                .done(function (likes) {
                    $(` #likecount-${id}`).text(likes);
                })
        })
    }, 1000);


});

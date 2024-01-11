export const getLabels = (interval = 15) => {

    let dayAgo = new Date();
    dayAgo.setDate(dayAgo.getDate() - 1);
    dayAgo.setMinutes(0);



    var labels = [];

    for (let i = 0; i < 24 * 60; i += interval) {

        dayAgo.setMinutes(dayAgo.getMinutes() + interval);

        const hour = dayAgo.getHours();
        const minute = dayAgo.getMinutes();
        const label = `${hour.toString().padStart(2, '0')}:${minute.toString().padStart(2, '0')}`

        labels.push(label)
    }

    return labels;
}
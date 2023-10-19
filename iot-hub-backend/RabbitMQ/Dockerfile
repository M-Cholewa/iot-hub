FROM rabbitmq:3-management-alpine

# W³¹cz plugin MQTT
RUN rabbitmq-plugins enable rabbitmq_mqtt
ENV RABBIT_IOT_USER mqtt-test
ENV RABBIT_IOT_PWD mqtt-test
ENV RABBITMQ_PID_FILE /var/lib/rabbitmq/mnesia/rabbitmq

COPY RabbitMQ/init.sh /init.sh
RUN chmod +x /init.sh
EXPOSE 15672

CMD ["/init.sh"]

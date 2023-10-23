FROM rabbitmq:3-management-alpine

# W³¹cz plugin MQTT
RUN rabbitmq-plugins enable rabbitmq_mqtt

COPY RabbitMQ/init.sh /init.sh
RUN chmod +x /init.sh
EXPOSE 15672

CMD ["/init.sh"]
